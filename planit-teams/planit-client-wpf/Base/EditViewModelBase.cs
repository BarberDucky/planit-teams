using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Base
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        public CommandBase SubmitCommand { get; protected set; }

        private Action<IEditable> SubmitAction;

        public EditViewModelBase(Action<IEditable> onSubmit)
        {
            SubmitAction = onSubmit;
            SubmitCommand = new CommandBase(OnSubmit);
        }

        public void OnSubmit()
        {
            bool succ = ValidateInstance();
            if(succ == true)
            {
                IEditable model = GetInstance();
                SubmitAction?.Invoke(model);
            }

        }

        public abstract bool ValidateInstance();
        public abstract IEditable GetInstance();

    }
}
