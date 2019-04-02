using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class EditBoardViewModel : EditViewModelBase
    {
        private EditBoard board;

        public EditBoard Board
        {
            get { return board; }
            set
            {
                SetProperty(ref board, value);
                //RaiseValidateInstanceChanged();
            }
        }

        public EditBoardViewModel(Action<IEditable> onSubmit, EditBoard board)
            :base(onSubmit)
        {
            this.Board = board;
        }

        public override IEditable GetInstance()
        {
            return board;
        }

        public override bool ValidateInstance()
        {
            return board != null && !String.IsNullOrWhiteSpace(board.Name);
        }

    }
}
