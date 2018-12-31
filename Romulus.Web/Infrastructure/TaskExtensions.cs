namespace Romulus.Web
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static ConfiguredTaskAwaitable WithoutCapturingContext(this Task task) => task.ConfigureAwait(false);

        public static ConfiguredTaskAwaitable<T> WithoutCapturingContext<T>(this Task<T> task) => task.ConfigureAwait(false);
    }
}
