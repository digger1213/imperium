#region

using System;
using System.Collections.Generic;

#endregion

namespace Imperium.Util.Binding;

/// <summary>
///     Binds a value and allows subscribers to register on change listeners.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ImpBinding<T> : IBinding<T>
{
    public event Action<T> onUpdate;
    public event Action<T> onUpdateFromLocal;
    public event Action onTrigger;
    public event Action onTriggerFromLocal;

    private readonly bool ignoreRefresh;

    public T DefaultValue { get; }
    public T Value { get; protected set; }

    public ImpBinding()
    {
    }

    public ImpBinding(
        T currentValue,
        T defaultValue = default,
        Action<T> onUpdate = null,
        Action<T> onUpdateFromLocal = null,
        bool ignoreRefresh = false
    )
    {
        Value = currentValue;
        DefaultValue = EqualityComparer<T>.Default.Equals(defaultValue, default)
            ? defaultValue
            : currentValue;

        this.ignoreRefresh = ignoreRefresh;

        this.onUpdate += onUpdate;
        this.onUpdateFromLocal += onUpdateFromLocal;
    }

    public void Refresh()
    {
        if (!ignoreRefresh) Set(Value);
    }

    public void Reset(bool invokeUpdate = true) => Set(DefaultValue, invokeUpdate);

    public virtual void Set(T updatedValue, bool invokeUpdate = true)
    {
        var isSame = EqualityComparer<T>.Default.Equals(Value, Value);
        Value = updatedValue;

        if (invokeUpdate)
        {
            onUpdate?.Invoke(Value);
            onTrigger?.Invoke();

            if (!isSame)
            {
                onUpdateFromLocal?.Invoke(updatedValue);
                onTriggerFromLocal?.Invoke();
            }
        }
    }

    public void Clear()
    {

    }

    // public void Clear()
    // {
    //     var onUpdateList = onUpdate?.GetInvocationList();
    //     if (onUpdateList != null)
    //     {
    //         foreach (var callback in onUpdateList)
    //         {
    //             onUpdate -= (Action<T>)callback;
    //         }
    //     }
    //
    //     var onTriggerList = onUpdate?.GetInvocationList();
    //     if (onTriggerList != null)
    //     {
    //         foreach (var callback in onTriggerList)
    //         {
    //             onUpdate -= (Action<T>)callback;
    //         }
    //     }
    //     onTrigger = null;
    //     onTriggerFromLocal = null;
    //     onUpdateFromLocal = null;
    // }
}