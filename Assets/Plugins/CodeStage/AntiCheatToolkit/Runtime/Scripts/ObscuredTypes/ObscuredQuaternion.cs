﻿#region copyright
// ------------------------------------------------------
// Copyright (C) Dmitriy Yukhanov [https://codestage.net]
// ------------------------------------------------------
#endregion

namespace CodeStage.AntiCheat.ObscuredTypes
{
	using System;
	using UnityEngine;
	using Utils;
	using Detectors;

	/// <summary>
	/// Use it instead of regular <c>Quaternion</c> for any cheating-sensitive variables.
	/// </summary>
	/// <strong>\htmlonly<font color="FF4040">WARNING:</font>\endhtmlonly Doesn't mimic regular type API, thus should be used with extra caution.</strong> Cast it to regular, not obscured type to work with regular APIs.<br/>
	/// <strong><em>Regular type is faster and memory wiser comparing to the obscured one!</em></strong><br/>
	/// Feel free to use regular types for all short-term operations and calculations while keeping obscured type only at the long-term declaration (i.e. class field).
	[Serializable]
	public partial struct ObscuredQuaternion : IObscuredType
	{
		private static readonly Quaternion Identity = Quaternion.identity;

#if UNITY_EDITOR
		public string migratedVersion;
#endif
		[SerializeField] internal int currentCryptoKey;
		[SerializeField] internal RawEncryptedQuaternion hiddenValue;
		[SerializeField] internal Quaternion fakeValue;
		[SerializeField] internal bool fakeValueActive;
		[SerializeField] internal bool inited;

		private ObscuredQuaternion(Quaternion value)
		{
			currentCryptoKey = GenerateKey();
			hiddenValue = Encrypt(value, currentCryptoKey);

#if UNITY_EDITOR
			fakeValue = value;
			fakeValueActive = true;
			migratedVersion = null;
#else
			var detectorRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = detectorRunning ? value : Identity;
			fakeValueActive = detectorRunning;
#endif
			inited = true;
		}

		/// <summary>
		/// Mimics constructor of regular Quaternion. Please note, passed components are not Euler Angles.
		/// </summary>
		/// <param name="x">X component of the quaternion</param>
		/// <param name="y">Y component of the quaternion</param>
		/// <param name="z">Z component of the quaternion</param>
		/// <param name="w">W component of the quaternion</param>
		public ObscuredQuaternion(float x, float y, float z, float w)
		{
			currentCryptoKey = GenerateKey();
			hiddenValue = Encrypt(x, y, z, w, currentCryptoKey);

			if (ObscuredCheatingDetector.ExistsAndIsRunning)
			{
				fakeValue = new Quaternion(x, y, z, w);
				fakeValueActive = true;
			}
			else
			{
				fakeValue = Identity;
				fakeValueActive = false;
			}

#if UNITY_EDITOR
			migratedVersion = null;
#endif
			inited = true;
		}

		/// <summary>
		/// Encrypts passed value using passed key.
		/// </summary>
		/// Key can be generated automatically using GenerateKey().
		/// \sa Decrypt(), GenerateKey()
		public static RawEncryptedQuaternion Encrypt(Quaternion value, int key)
		{
			return Encrypt(value.x, value.y, value.z, value.w, key);
		}

		/// <summary>
		/// Encrypts passed components using passed key.
		/// Please note, passed components are not an Euler Angles.
		/// </summary>
		/// Key can be generated automatically using GenerateKey().
		/// \sa Decrypt(), GenerateKey()
		public static RawEncryptedQuaternion Encrypt(float x, float y, float z, float w, int key)
		{
			RawEncryptedQuaternion result;
			result.x = ObscuredFloat.Encrypt(x, key);
			result.y = ObscuredFloat.Encrypt(y, key);
			result.z = ObscuredFloat.Encrypt(z, key);
			result.w = ObscuredFloat.Encrypt(w, key);

			return result;
		}

		/// <summary>
		/// Decrypts passed value you got from Encrypt() using same key.
		/// </summary>
		/// \sa Encrypt()
		public static Quaternion Decrypt(RawEncryptedQuaternion value, int key)
		{
			Quaternion result;
			result.x = ObscuredFloat.Decrypt(value.x, key);
			result.y = ObscuredFloat.Decrypt(value.y, key);
			result.z = ObscuredFloat.Decrypt(value.z, key);
			result.w = ObscuredFloat.Decrypt(value.w, key);

			return result;
		}

		/// <summary>
		/// Creates and fills obscured variable with raw encrypted value previously got from GetEncrypted().
		/// </summary>
		/// Literally does same job as SetEncrypted() but makes new instance instead of filling existing one,
		/// making it easier to initialize new variables from saved encrypted values.
		///
		/// <param name="encrypted">Raw encrypted value you got from GetEncrypted().</param>
		/// <param name="key">Encryption key you've got from GetEncrypted().</param>
		/// <returns>New obscured variable initialized from specified encrypted value.</returns>
		/// \sa GetEncrypted(), SetEncrypted()
		public static ObscuredQuaternion FromEncrypted(RawEncryptedQuaternion encrypted, int key)
		{
			var instance = new ObscuredQuaternion();
			instance.SetEncrypted(encrypted, key);
			return instance;
		}

		/// <summary>
		/// Generates random key. Used internally and can be used to generate key for manual Encrypt() calls.
		/// </summary>
		/// <returns>Key suitable for manual Encrypt() calls.</returns>
		public static int GenerateKey()
		{
			return RandomUtils.GenerateIntKey();
		}

		private static bool Compare(Quaternion q1, Quaternion q2)
		{
			var epsilon = ObscuredCheatingDetector.ExistsAndIsRunning ?
				ObscuredCheatingDetector.Instance.quaternionEpsilon : float.Epsilon;
			return NumUtils.CompareFloats(q1.x, q2.x, epsilon) &&
			       NumUtils.CompareFloats(q1.y, q2.y, epsilon) &&
			       NumUtils.CompareFloats(q1.z, q2.z, epsilon) &&
			       NumUtils.CompareFloats(q1.w, q2.w, epsilon);
		}

		/// <summary>
		/// Allows to pick current obscured value as is.
		/// </summary>
		/// <param name="key">Encryption key needed to decrypt returned value.</param>
		/// <returns>Encrypted value as is.</returns>
		/// Use it in conjunction with SetEncrypted().<br/>
		/// Useful for saving data in obscured state.
		/// \sa FromEncrypted(), SetEncrypted()
		public RawEncryptedQuaternion GetEncrypted(out int key)
		{
			if (!inited)
				Init();
			
			key = currentCryptoKey;
			return hiddenValue;
		}

		/// <summary>
		/// Allows to explicitly set current obscured value. Crypto key should be same as when encrypted value was got with GetEncrypted().
		/// </summary>
		/// Use it in conjunction with GetEncrypted().<br/>
		/// Useful for loading data stored in obscured state.
		/// \sa FromEncrypted()
		public void SetEncrypted(RawEncryptedQuaternion encrypted, int key)
		{
			inited = true;
			hiddenValue = encrypted;
			currentCryptoKey = key;

			if (ObscuredCheatingDetector.ExistsAndIsRunning)
			{
				fakeValueActive = false;
				fakeValue = InternalDecrypt();
				fakeValueActive = true;
			}
			else
			{
				fakeValueActive = false;
			}
		}

		/// <summary>
		/// Alternative to the type cast, use if you wish to get decrypted value
		/// but can't or don't want to use cast to the regular type.
		/// </summary>
		/// <returns>Decrypted value.</returns>
		public Quaternion GetDecrypted()
		{
			return InternalDecrypt();
		}

		public void RandomizeCryptoKey()
		{
			var decrypted = InternalDecrypt();
			currentCryptoKey = GenerateKey();
			hiddenValue = Encrypt(decrypted, currentCryptoKey);
		}

		private Quaternion InternalDecrypt()
		{
			if (!inited)
			{
				Init();
				return Identity;
			}

			var decrypted = Decrypt(hiddenValue, currentCryptoKey);

			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && !Compare(decrypted, fakeValue))
			{
#if ACTK_DETECTION_BACKLOGS
				Debug.LogWarning(ObscuredCheatingDetector.LogPrefix + "Detection backlog:\n" +
				                             $"type: {nameof(ObscuredQuaternion)}\n" +
				                             $"decrypted: {decrypted}\n" +
				                             $"fakeValue: {fakeValue}\"" +
				                             $"epsilon: {ObscuredCheatingDetector.Instance.quaternionEpsilon}\n" +
				                             $"compare: {Compare(decrypted, fakeValue)}");
#endif
				ObscuredCheatingDetector.Instance.OnCheatingDetected(this, decrypted, fakeValue);
			}

			return decrypted;
		}
		
		private void Init()
		{
			currentCryptoKey = GenerateKey();
			hiddenValue = Encrypt(Identity, currentCryptoKey);
			fakeValue = Identity;
			fakeValueActive = false;
			inited = true;
		}

		#region obsolete
		
		//! @cond

		[Obsolete("This API is redundant and does not perform any actions. It will be removed in future updates.")]
		public static void SetNewCryptoKey(int newKey) {}

		[Obsolete("This API is redundant and does not perform any actions. It will be removed in future updates.")]
		public void ApplyNewCryptoKey() {}

		[Obsolete("Please use new Encrypt(value, key) API instead.", true)]
		public static RawEncryptedQuaternion Encrypt(Quaternion value) { throw new Exception(); }

		[Obsolete("Please use new Decrypt(value, key) API instead.", true)]
		public static Quaternion Decrypt(RawEncryptedQuaternion value) { throw new Exception(); }

		[Obsolete("Please use new GetEncrypted(out key) API instead.", true)]
		public RawEncryptedQuaternion GetEncrypted() { throw new Exception(); }

		[Obsolete("Please use new SetEncrypted(encrypted, key) API instead.", true)]
		public void SetEncrypted(RawEncryptedQuaternion encrypted) {}
		
		//! @endcond

		#endregion
	}
}