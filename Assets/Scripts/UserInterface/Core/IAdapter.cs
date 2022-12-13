namespace UserInterface.Core
{
    public interface IAdapter<TView, TModel> where TView : IView
    {
        public TView View { get; set; }
        public TModel Model { get; set; }
    }
}