﻿#region copyright
// ------------------------------------------------------
// Copyright (C) Dmitriy Yukhanov [https://codestage.net]
// ------------------------------------------------------
#endregion

#if UNITY_EDITOR
namespace CodeStage.AntiCheat.ObscuredTypes
{
    public partial struct ObscuredQuaternion
    {
        internal bool IsDataValid
        {
            get
            {
                if (!inited || !fakeValueActive) return true;
                return Compare(fakeValue, InternalDecrypt());
            }
        }
    }
}
#endif