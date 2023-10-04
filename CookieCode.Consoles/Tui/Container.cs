namespace CookieCode.Consoles.Tui
{
    public abstract class Container : Control
    {
        public abstract IEnumerable<Control> Children { get; }

        public override void Render(RenderContext context)
        {
            foreach (var child in Children)
            {
                if (child.IsVisible)
                {
                    child.Render(context);
                }
            }
        }
    }

    public static class ContainerExtensionMethods
    {
        //public static TContainer AddChild<TContainer>(this TContainer container, Control control)
        //    where TContainer : Container
        //{
        //    if (control.Parent != null)
        //    {
        //        control.Parent.RemoveChild(control);
        //    }
        //
        //    container._children.Add(control);
        //    control.Parent = container;
        //
        //    return container;
        //}

        //public static TContainer RemoveChild<TContainer>(this TContainer container, Control control)
        //    where TContainer : Container
        //{
        //    if (container._children.Contains(control))
        //    {
        //        container._children.Remove(control);
        //        control.Parent = null;
        //    }
        //
        //    return container;
        //}

        public static IEnumerable<Control> Flatten(this Container container)
        {
            yield return container;

            foreach (var control in container.Children)
            {
                var childContainer = control as Container;
                if (childContainer != null)
                {
                    foreach (var item in Flatten(childContainer))
                    {
                        yield return item;
                    }
                }
                else
                {
                    yield return control;
                }
            }
        }
    }
}
