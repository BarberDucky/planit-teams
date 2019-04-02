using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.MQ;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class CommentsViewModel : ViewModelBase
    {
        private ObservableCollection<ReadComment> comments;
        private int cardId;
        private string newComment;

        #region Properties

        public ObservableCollection<ReadComment> Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        public string NewComment
        {
            get { return newComment; }
            set
            {
                SetProperty(ref newComment, value);
                NewCommentCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public CommandBase NewCommentCommand { get; protected set; }

        #endregion

        public CommentsViewModel(ObservableCollection<ReadComment> comments, int cardId)
        {
            this.cardId = cardId;
            this.comments = comments;
            NewCommentCommand = new CommandBase(OnNewComment, CanNewComment);
        }

        private async void OnNewComment()
        {
            if (ActiveUser.IsActive == true && !String.IsNullOrWhiteSpace(NewComment))
            {
                CreateCommentDTO createCommentDTO = new CreateCommentDTO() { Text = NewComment, CardId = cardId };
                BasicCommentDTO basicCommentDTO = await CommentService.CreateComment(ActiveUser.Instance.LoggedUser.Token, createCommentDTO);

                if (basicCommentDTO != null)
                {
                    var comment = new ReadComment(basicCommentDTO);
                    Comments.Add(comment);
                    NewComment = "";
                }
                else
                {
                    ShowMessageBox(null, "Error creating list.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        private bool CanNewComment()
        {
            return !String.IsNullOrWhiteSpace(NewComment);
        }

        
    }
}
