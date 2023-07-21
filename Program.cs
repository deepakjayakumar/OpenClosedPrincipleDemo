namespace OpenClosedPrincipleDemo
{
    internal class Program
    {
        public enum Color
        {
            Red,Green, Blue
        }
        public enum Size
        {
            Small,Medium,Large
        }

        public class Product
        {
            public string Name;
            public Color Color;
            public Size Size;

            public Product(string name, Color color, Size size)
            {
                  if(name == null)
                    throw new ArgumentNullException("name");

                Name = name;  
                Color = color;  
                Size = size;
            }   
        }

        public interface ISpecification<T>
        { 
            bool IsSatisfiedBy(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items,ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color _color;
            public ColorSpecification(Color color)
            {   
                _color = color;
                    
            }
            public bool IsSatisfiedBy(Product product)
            {
                return product.Color == _color;
                  
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            
            private Size _size;

            public SizeSpecification(Size size)
            {
                _size = size;
            }

            public bool IsSatisfiedBy(Product product)
            { return product.Size == _size; }

        }


        public class AndSpecification<T> : ISpecification<T>
        {
            ISpecification<T> _first, _second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                _first = first;
                _second = second;
            }

            public bool IsSatisfiedBy(T t)
            {
                 return _first.IsSatisfiedBy(t) && _second.IsSatisfiedBy(t);
            }
        }

        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach(var p in items)
                {
                    if(spec.IsSatisfiedBy(p))
                        yield return p;
                }
            }
        }
        public class ProductFilter
        {

            public  IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size) {

                foreach (Product p in products)
                {
                    if(p.Size == size)
                    yield return p;
                }
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {

                foreach (Product p in products)
                {
                    if (p.Color == color)
                        yield return p;
                }
            }

            public IEnumerable<Product> FilterBySizandColor(IEnumerable<Product> products, Color color,Size size)
            {

                foreach (Product p in products)
                {
                    if (p.Color == color && p.Color == color)
                        yield return p;
                }
            }




        }



        static void Main(string[] args)
        {
            var apple = new Product("apple", Color.Red, Size.Small);
            var tree = new Product("tree", Color.Blue, Size.Medium);
            var house = new Product("house", Color.Blue, Size.Large);

            Product[] products = new Product[] {apple, tree, house};

            var pf = new ProductFilter();
            foreach(var p in pf.FilterByColor(products,Color.Red)) 
            {
                Console.WriteLine($"{p.Name}" + " is red");
            }

            var bf = new BetterFilter();
            foreach(var p in bf.Filter(products,new AndSpecification<Product>(new ColorSpecification(Color.Blue),new SizeSpecification(Size.Medium))))
            {
                Console.WriteLine($"{p.Name}" + " is blue and medium");
            }
                

            
        }
    }
}