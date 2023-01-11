namespace UserInterface.Core
{
    public interface IAdapter<out TView, TModel> where TView : IView
    {
        public TView View { get; }
        public TModel Model { get; set; }
    }
}