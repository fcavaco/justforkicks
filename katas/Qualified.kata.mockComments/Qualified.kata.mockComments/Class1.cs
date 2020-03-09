using System;
public class Challenge
{
    public class User
    {
        private string _name;
        private bool _isLoggedIn;
        private DateTime _lastLoggedInAt;

        public User(string name)
        {
            _name = name;
            _isLoggedIn = false;
            _lastLoggedInAt = DateTime.MinValue;
        }

        public bool IsLoggedIn()
        {
            return _isLoggedIn;
        }

        public DateTime GetLastLoggedInAt()
        {
            return _lastLoggedInAt;
        }

        public void LogIn()
        {
            _isLoggedIn = true;
            _lastLoggedInAt = DateTime.Now;
        }
        public void LogOut()
        {
            _isLoggedIn = false;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public virtual bool CanEdit(Comment comment)
        {
            if (comment.GetAuthor() == this)
                return true;
            return false;
        }
        public virtual bool CanDelete(Comment comment)
        {
            return false;
        }
    }

    public class Moderator : User
    {
        public Moderator(String name) : base(name)
        {
        }

        public override bool CanDelete(Comment comment)
        {
            return true;
        }
    }

    public class Admin : Moderator
    {
        public Admin(String name) : base(name)
        {

        }
        public override bool CanEdit(Comment comment)
        {
            return true;
        }
        public override bool CanDelete(Comment comment)
        {
            return true;
        }
    }

    public class Comment
    {

        private readonly DateTime _createdAt;
        private readonly User _author;
        private readonly Comment _repliedTo;
        private string _message;
        public Comment(User author, string message, Comment repliedTo = null)
        {
            _repliedTo = repliedTo;
            _author = author;
            _createdAt = DateTime.Now;
            _message = message;
        }

        public string GetMessage()
        {
            return _message;
        }
        public void SetMessage(String message) 
        { 
            _message = message; 
        }

        public DateTime GetCreatedAt()
        {
            return _createdAt;
        }

        public User GetAuthor()
        {
            return _author;
        }

        public Comment GetRepliedTo()
        {
            return _repliedTo;
        }

        public override string ToString()
        {
            if (GetRepliedTo() != null)
            {
                return $"{GetMessage()} by {GetAuthor().GetName()}";
            }
            else
            {
                return $"{GetMessage()} by {GetAuthor().GetName()} (replied to {GetRepliedTo().GetAuthor().GetName()})";
            }
        }
    }
}