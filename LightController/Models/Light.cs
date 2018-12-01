namespace LightController.Models
{
    public class Light
    {
        public string etag { get; set; }
        public bool hascolor { get; set; }
        public string manufacturername { get; set; }
        public string modelid { get; set; }
        public string name { get; set; }
        public State state { get; set; }
        public string swversion { get; set; }
        public string type { get; set; }
        public string uniqueid { get; set; }
    }
}