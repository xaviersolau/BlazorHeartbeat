using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace SoloX.BlazorHeartbeat
{
    public partial class Heartbeat : IDisposable
    {
        private int count;
        private CancellationTokenSource cancellationTokenSource;

        public Heartbeat()
        {
            this.count = 0;

            PingDelay = 60_000;
        }

        [Parameter]
        public int PingDelay { get; set; }

        public void Dispose()
        {
            if (this.cancellationTokenSource != null)
            {
                this.cancellationTokenSource.Cancel();
                this.cancellationTokenSource.Dispose();
                this.cancellationTokenSource = null;
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                this.cancellationTokenSource = new CancellationTokenSource();

                this.Ping(this.cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }

        private async Task Ping(CancellationToken cancellationToken)
        {
            try
            {
                for (; ; )
                {
                    await Task.Delay(PingDelay, cancellationToken);

                    count++;

                    await this.InvokeAsync(() =>
                    {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            this.StateHasChanged();
                        }
                    });
                }

            }
            catch (TaskCanceledException)
            {
                // Nothing to do...
            }
        }
    }
}
