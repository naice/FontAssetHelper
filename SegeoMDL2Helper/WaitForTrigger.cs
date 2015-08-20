using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FontAssetHelper
{
    public class WaitForTrigger
    {
        public int DelayInMillis { get; private set; }
        public Action<object> ActionParameterized { get; set; }
        public Action Action { get; set; }
        public object Tag { get; set; }

        private CancellationTokenSource _tokenSource = null;
        private Dispatcher _dispatcher;

        public WaitForTrigger(int delayInMillis, Action<object> action)
            : this(delayInMillis, App.Current.Dispatcher)
        {
            this.ActionParameterized = action;
        }
        public WaitForTrigger(int delayInMillis, Action action, Dispatcher dispatcher)
            : this(delayInMillis, dispatcher)
        {
            this.Action = action;
        }
        private WaitForTrigger(int delayInMillis, Dispatcher dispatcher)
        {
            this.DelayInMillis = delayInMillis;
            this._dispatcher = dispatcher;
        }

        public void Trigger()
        {
            trigger(null);
        }

        public void Trigger(object param)
        {
            trigger(param);
        }

        [DebuggerNonUserCode]
        private async Task task(CancellationToken token, object param)
        {
            bool cancelled = false;
            try
            {
                await Task.Delay(DelayInMillis, token);
            }
            catch (TaskCanceledException)
            {
            }

            cancelled = token.IsCancellationRequested;

            if (!cancelled)
            {
                Action action = new Action(() => {
                    if (ActionParameterized != null)
                        ActionParameterized.Invoke(param);
                    else if (Action != null)
                        Action.Invoke();
                });

                if (_dispatcher == null || _dispatcher.Thread == Thread.CurrentThread)
                {
                    action();
                }
                else
                {
                    await _dispatcher.InvokeAsync(() =>
                    {
                        if (ActionParameterized != null)
                            ActionParameterized.Invoke(param);
                        else if (Action != null)
                            Action.Invoke();
                    });
                }
            }
        }

        [DebuggerNonUserCode]
        private async void trigger(object param)
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }

            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;


            await task(token, param);
        }
    }
}
