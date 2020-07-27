namespace GreenMap.Models
{
    public class Layer
    {
        public string FullName { get; set; }
        public bool Visible { get; set; }
        public string Color { get; set; }
        public double Opacity { get; set; }
        public string Stroke { get; set; }
        public string Text { get; set; }
        public bool Cluster { get; set; }
        public int Radius { get; set; }
        public int IconShapePoints { get; set; }
        public string UrlWMS { get; set; }
        public string LayersWMS { get; set; }
    }
}
