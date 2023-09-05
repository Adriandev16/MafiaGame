namespace MafiaGame.Interfaces
{
    public interface IViewModelWithParameters
    {
        public void SetParameters(IDictionary<string, object> routeParameters);
    }
}
