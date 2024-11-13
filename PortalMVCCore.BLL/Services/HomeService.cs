using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.DTOs;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;

namespace PortalMVCCore.BLL.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _repository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IProgramasRepository _programasRepository;

        public HomeService(IHomeRepository repository, IUsuariosRepository usuariosRepository, IProgramasRepository programasRepository)
        {
            _repository = repository;
            _usuariosRepository = usuariosRepository;
            _programasRepository = programasRepository;
        }

        public List<DadosDashBoardDTO> GetDashBoard()
        {
            return _repository.GetDashBoard();
        }

        public async Task<DadosHeaderDTO> GetDadosHeader(string NomeController, int IdUsuario, int IdTipoLogin)
        {
            DadosHeaderDTO Dados = new DadosHeaderDTO();

            USUARIOS_TAB? usuarios_tab = await _usuariosRepository.FindAsync(IdUsuario);
            Dados.IdUsuario = "0";
            Dados.Usuario = "";

            if (usuarios_tab != null)
            {
                Dados.IdUsuario = usuarios_tab.ID_USUARIO.ToString();
                Dados.Usuario = usuarios_tab.NOME != null ? (usuarios_tab.NOME?.Length <= 26 ? usuarios_tab.NOME : usuarios_tab.NOME?.Substring(0, 26)) : "";
            }

            PROGRAMAS_TAB? programas_tab = await _programasRepository.FirstOrDefaultAsync(p => p.NOME_CONTROLLER == NomeController);
            Dados.DescricaoController = programas_tab != null ? programas_tab.DESCRICAO : "";

            Dados.IdTipoLogin = IdTipoLogin;

            return Dados;
        }
    }
}
