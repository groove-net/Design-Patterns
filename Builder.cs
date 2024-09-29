namespace HelloWorld
{
    public interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        void BuildPartC();
    }

    public class Product
    {
        private readonly List<object> _parts = new();

        public void Add(string part)
        {
            this._parts.Add(part);
        }

        public string ListParts()
        {
            string str = string.Empty;
            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }
            str = str.Remove(str.Length - 2);
            return "Product parts: " + str + "\n";
        }
    }

    // public class Director
    // {
    //     private readonly IBuilder _builder;

    //     public IBuilder Builder
    //     {
    //         set {_builder = value;}
    //     }

    //     public void BuildMinimalViableProduct()
    //     {
    //         this._builder.BuildPartA;
    //     }

    // }

    public class ConcreteBuilder : IBuilder
    {
        private Product _product = new();
        public ConcreteBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._product = new Product();
        }

        public void BuildPartA()
        {
            this._product.Add("PartA");
        }
        public void BuildPartB()
        {
            this._product.Add("PartB");
        }
        public void BuildPartC()
        {
            this._product.Add("PartC");
        }

        public Product GetProduct()
        {
            Product result = this._product;
            this.Reset();
            return result;
        }
    }
}