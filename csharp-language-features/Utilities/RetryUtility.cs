using System;
using System.Threading;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class Retry
    {
        public static double DefaultTimeout = System.TimeSpan.FromSeconds(5).TotalMilliseconds;
        public static double MaxSleepTime = TimeSpan.FromSeconds(1).TotalMilliseconds;

        /// <summary>
        /// Executes a function until it succeeds or times out. Success determined by no error
        /// occuring during the invocation of the <paramref name="method"/>
        /// </summary>
        /// <param name="method">method to execute</param>
        /// /// <param name="onError">optional action to invoke if an error occurs</param>
        /// <param name="timeoutMilliseconds">timeout, defaults to 5 seconds</param>
        public static void Do(Action method, Action<Exception> onError = null, double? timeoutMilliseconds = null)
        {
            timeoutMilliseconds = timeoutMilliseconds ?? DefaultTimeout;
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            var sleepTime = 100;

            do
            {
                try
                {
                    method.Invoke();                    
                }
                catch (Exception exception)
                {
                    if (onError != null)
                    {
                        onError.Invoke(exception);
                    }

                    Thread.Sleep(sleepTime);
                    if (sleepTime < MaxSleepTime)
                    {
                        sleepTime = sleepTime + sleepTime; // increase waits between retry attempts
                    }                    
                }
            }

            while (timer.ElapsedMilliseconds <= timeoutMilliseconds);            
        }
        /// <summary>
        /// Executes a function until it succeeds or times out. Success determined by no error
        /// occuring during the invocation of the <paramref name="method"/>
        /// </summary>
        /// <param name="method">method to execute</param>
        /// /// <param name="onError">optional action to invoke if an error occurs</param>
        /// <param name="timeoutMilliseconds">timeout, defaults to 5 seconds</param>
        public static ExecuteResult<T> Do<T>(Func<T> method, Action<Exception> onError = null, double? timeoutMilliseconds = null)
        {
            timeoutMilliseconds = timeoutMilliseconds ?? DefaultTimeout;
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            var sleepTime = 100;
            var result = new ExecuteResult<T>();
            do
            {
                try
                {
                    result.Result = method.Invoke();
                    return result;
                }
                catch (Exception exception)
                {
                    result.LastException = exception;
                    if (onError != null)
                    {
                        onError.Invoke(exception);
                    }

                    Thread.Sleep(sleepTime);
                    if (sleepTime < MaxSleepTime)
                    {
                        sleepTime = sleepTime + sleepTime; // increase waits between retry attempts
                    }                    
                }
            }

            while (timer.ElapsedMilliseconds <= timeoutMilliseconds);
            return result;
        }

        /// <summary>
        /// Executes a function until it succeeds or times out.
        /// </summary>
        /// <typeparam name="T">value to return from method</typeparam>
        /// <param name="method">method which returns a bool to determine success and a value T</param>
        /// <param name="onError">action to invoke if an error occurs</param>
        /// <param name="timeoutMilliseconds">timeout, defaults to 5 seconds</param>
        /// <returns></returns>
        public static ExecuteResult<T> Do<T>(Func<Tuple<bool, T>> method, Action<Exception> onError = null, double? timeoutMilliseconds = null)
        {
            var timer = new System.Diagnostics.Stopwatch();
            timeoutMilliseconds = timeoutMilliseconds ?? DefaultTimeout;
            timer.Start();
            var sleepTime = 100;
            var result = new ExecuteResult<T>();
            do
            {
                try
                {
                    var invokeResult = method.Invoke();
                    bool funcResult = invokeResult.Item1;
                    if (funcResult)
                    {
                        result.Result = invokeResult.Item2;
                        return result;
                    }
                }
                catch (Exception exception)
                {
                    result.LastException = exception;
                    if (onError != null)
                    {
                        onError.Invoke(exception);
                    }
                }
                finally
                {
                    if (result.ErrorOccured)
                    {
                        Thread.Sleep(sleepTime);
                        if (sleepTime < MaxSleepTime)
                        {
                            sleepTime = sleepTime + sleepTime; // increase waits between retry attempts                    
                        }
                    }
                }
            }

            while (timer.ElapsedMilliseconds <= timeoutMilliseconds);
            return result;
        }
    }

    public class ExecuteResult<T>
    {
        public Exception LastException { get; set; }

        public T Result { get; set; }

        public bool ErrorOccured
        {
            get
            {
                return this.LastException != null;
            }
        }
    }
}