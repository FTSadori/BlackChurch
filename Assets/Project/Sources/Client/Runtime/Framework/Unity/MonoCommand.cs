using Client.Runtime.Framework.Command;
using UnityEngine;

namespace Client.Runtime.Framework.Unity
{
    public abstract class MonoCommand : MonoBehaviour, ICommand
    {
        public abstract void Execute();
    }
    
    public abstract class MonoCommand<TOutput> : MonoBehaviour, ICommand<TOutput>
    {
        public abstract TOutput Execute();
    }

    public abstract class MonoCommand<TInput, TOutput> : MonoBehaviour, ICommand<TInput, TOutput>
    {
        public abstract TOutput Execute(TInput data);
    }
}
