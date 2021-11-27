using SQLite;

namespace CodeCave.Model
{
    public class Note
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImagePath { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool CanShowImage { get; set; }
        public bool CanSendNotification { get; set; }
        public bool RecievedNotification { get; set; }
    }
}
