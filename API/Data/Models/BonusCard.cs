namespace spp3.Data.Models
{
    public partial class BonusCard
    {
        public static int autoInc = 1;

        public int bcId { get; set; }

        public string number { get; set; }

        public float discount { get; set; }

        public int cuId { get; set; }

        public virtual Customer? Customer { get; set; }

        public BonusCard()
        {
            this.number = String.Format("{0:0000000000000000}", autoInc++);
        }

        public BonusCard(float discount, int cuId)
        {
            this.discount = discount;
            this.cuId = cuId;
        }

        public override string ToString()
        {
            return String.Format("number:{0}", number);
        }
    }
}
