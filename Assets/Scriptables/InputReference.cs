using System;
using UnityEngine;

namespace CustomInputControl
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Input", menuName = "Scriptable Object/Input")]
    public class InputReference : ScriptableObject
    {
        public bool up, left, down, right;
        public bool dash, jump;
        public bool slash, heavySlash;
    }
}