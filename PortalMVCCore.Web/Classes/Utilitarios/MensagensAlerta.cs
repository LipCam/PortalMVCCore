namespace PortalMVCCore.Web.Classes.Utilitarios
{
    public class MensagensAlerta
    {
        public enum TipoMsg
        {
            Nothing = 0,
            Success = 1,
            Error = 2,
            Warning = 3,
            Info = 4,
            Confirm = 5//(Para este chamar o Lobibox.confirm)
        }

        public TipoMsg Tipo { get; set; } = TipoMsg.Nothing;
        public string Titulo { get; set; } = "";
        public string Mensagem { get; set; } = "";
    }
}
