using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Base
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        private bool isOpen;
        //BindableBase model; 

        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                SetProperty(ref isOpen, value);
            }
        }

        //public BindableBase Model
        //{
        //    get { return model; }
        //    set { SetProperty(ref model, value); }
        //}

        public CommandBase SubmitCommand { get; protected set; }

        private Func<BindableBase, bool> ExecuteSubmit;

        public EditViewModelBase(Func<BindableBase, bool> onExecute)
        {
            ExecuteSubmit = onExecute;
            //this.model = model;
        }

        public void OnSubmit()
        {
            bool succ = ValidateInstance();
            if(succ == true)
            {
                BindableBase model = GetInstance();
                ExecuteSubmit?.Invoke(model);
            }

        }

        public abstract bool ValidateInstance();
        public abstract BindableBase GetInstance();

    }
}
