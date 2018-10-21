using System;
using System.Linq;
using System.Collections.Generic;

namespace awssample
{
    public class Node
    {

        public Node parent;
        public List<int> point = new List<int>() {0,0};
        public List<Node> children = new List<Node>(); 
        public Node(){
            
        }



    }




    class MainClass
    {
        public static void generate_three(Node root, List<List<int>> locations, int levels)
        {
            if (levels == 0)
            {
                return;
            }
            foreach (var item in locations)
            {
                var tmp = new List<List< int>>(locations);
                tmp.Remove(item);
                var n = new Node();
                n.point = item;
                n.parent = root;
                root.children.Add(n);
                generate_three(n, tmp, levels - 1);
            }
        }

        public static float calculate_cost(Node n)
        {
            float cost = 0;
            while (n != null && n.parent != null)
            {
                cost = cost + distance(n.point, n.parent.point);
                n = n.parent;
            }
            return cost;
        }

        public static List<List< int>> generate_path(Node n)
        {
            var result = new List<List< int>>();
            while (n != null && n.parent != null)
            {
                result.Add(n.point);
                n = n.parent;
            }
            result.Reverse();
            return result;
        }
        public static void search(Node root, Dictionary<string, Node> path, Dictionary<string, float> cost)
        {

            if (root.children == null || root.children.Count() == 0)
            {
                if ( ! path.ContainsKey("path") )
                {
                    path["path"] = root;
                    cost["cost"] = calculate_cost(root);
                }
                else
                {
                    var best_cost = cost["cost"];
                    var current_cost = calculate_cost(root);
                    if (current_cost < best_cost)
                    {
                        cost["cost"] = current_cost;
                        path["path"] = root;
                    }
                }
            }
            else
            {
                foreach (var item in root.children)
                {
                    search(item, path, cost);
                }
            }
        }


        public static float distance(List< int> x, List< int> y)
        {
            return (float)Math.Sqrt(Math.Pow(x[0] - y[0], 2) + Math.Pow(x[1] - y[1], 2));
        }
        public static void Main(string[] args)
        {
            /*
            var locations = new List<List<int>>(  ){
                new List<int>(){ 1, 2 },
                new List<int>(){ 3, 4},
                new List<int>(){ 1, -1 }
            };
            */

            var locations = new List<List<int>>(){
                new List<int>(){ 3, 6 },
                new List<int>(){ 2, 4},
                new List<int>(){ 5, 3 },
                new List<int>(){ 2, 7 },
                new List<int>(){ 1, 8 },
                new List<int>(){ 7, 9 }
            };

            var root = new Node(); 

            generate_three(root, locations, 3);
            var path = new Dictionary<string,Node>();
            var cost = new Dictionary<string, float>();  
            search(root, path, cost);

            var res =  generate_path(path["path"]);

            foreach (var item in res)
            {
                Console.WriteLine(string.Format("{0} {1}", item[0], item[1]));
            }
        }
    }
}
