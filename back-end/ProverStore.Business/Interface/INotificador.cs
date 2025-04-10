using ProverStore.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface INotificador
    {
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
        void Handle(Notificacao notificacao);
    }
}
