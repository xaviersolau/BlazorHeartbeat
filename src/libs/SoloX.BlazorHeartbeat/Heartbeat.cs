// ----------------------------------------------------------------------
// <copyright file="Heartbeat.cs" company="Xavier Solau">
// Copyright © 2021 Xavier Solau.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace SoloX.BlazorHeartbeat
{
    /// <summary>
    /// Heartbeat is the component that is supposed to be used in a server side Blazor application.
    /// It will maintain your signalR connection up generating events at a given frequency.
    /// It is especially useful if your application is running behind an application gateway configured
    /// with a connection timeout.
    /// </summary>
    public sealed partial class Heartbeat : IDisposable
    {
        private int count;
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Setup Heartbeat component.
        /// </summary>
        public Heartbeat()
        {
            this.count = 0;

            PingDelay = 60_000;
        }

        /// <summary>
        /// Get/set the delay between each ping messages.
        /// </summary>
        [Parameter]
        public int PingDelay { get; set; }

        /// <summary>
        /// Dispose the component and cancel the ping task.
        /// </summary>
        public void Dispose()
        {
            if (this.cancellationTokenSource != null)
            {
                this.cancellationTokenSource.Cancel();
                this.cancellationTokenSource.Dispose();
                this.cancellationTokenSource = null;
            }
        }

        ///<inheritdoc/>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                this.cancellationTokenSource = new CancellationTokenSource();

                PingAsync(this.cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }

        private async Task PingAsync(CancellationToken cancellationToken)
        {
            try
            {
                for (; ; )
                {
                    await Task.Delay(PingDelay, cancellationToken).ConfigureAwait(false);

                    this.count++;

                    await InvokeAsync(() =>
                    {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            this.StateHasChanged();
                        }
                    }).ConfigureAwait(false);
                }

            }
            catch (TaskCanceledException)
            {
                // Nothing to do...
            }
        }
    }
}
