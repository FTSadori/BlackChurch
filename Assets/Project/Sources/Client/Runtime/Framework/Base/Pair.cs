using System;
using UnityEngine;

namespace Client.Framework.Base
{
    [Serializable]
	public struct Pair<Key, T> {
		[SerializeField] public Key key;
		[SerializeField] public T value;
	}
}