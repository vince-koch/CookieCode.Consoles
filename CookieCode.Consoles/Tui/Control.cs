using System.Drawing;

namespace CookieCode.Consoles.Tui
{
    public abstract class Control
    {
        public virtual bool IsVisible{ get; set; } = true;

        public virtual bool CanFocus { get; } = false;

        public Point? Cursor { get; protected set; }

        public Container? Parent { get; internal set; }

        public virtual void HandleKeyEvent(ConsoleKeyInfoEventArgs e)
        {
            HandleTabKey(e);
        }

        protected virtual void HandleTabKey(ConsoleKeyInfoEventArgs e)
        {
            if (e.Key.Key == ConsoleKey.Tab)
            {
                var application = this.GetParent<Application>();
                if (application != null)
                {
                    var flat = application.Flatten().Where(item => item.CanFocus).ToArray();
                    var currentFocus = application.Focus ?? application;
                    var currentFocusIndex = Array.IndexOf(flat, currentFocus);

                    var nextFocusIndex = e.Key.Modifiers.HasShift()
                        ? currentFocusIndex - 1
                        : currentFocusIndex + 1;

                    if (nextFocusIndex < 0)
                    {
                        nextFocusIndex = flat.Length - 1;
                    }
                    else if (nextFocusIndex >= flat.Length)
                    {
                        nextFocusIndex = 0;
                    }

                    var nextFocus = flat[nextFocusIndex];
                    application.Focus = nextFocus ?? currentFocus;

                    e.IsHandled = true;
                }
            }
        }

        public virtual void Render(RenderContext context)
        {
        }
    }

    public static class ControlExtensionMethods
    {
        public static TControl Configure<TControl>(this TControl control, Action<TControl> configure)
            where TControl : Control
        {
            configure(control);
            return control;
        }

        public static IEnumerable<Control> GetParents(this Control control)
        {
            yield return control;

            var current = control.Parent;
            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }

        public static TControl? GetParent<TControl>(this Control control)
            where TControl : Control
        {
            var parent = GetParents(control)
                .OfType<TControl>()
                .FirstOrDefault();

            return parent;
        }
    }
}
