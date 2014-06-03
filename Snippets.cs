using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bootpad {
    public class Snippets : XmlDocument {        
        static Snippets instance;

        private Snippets() { //make singleton          
            //empty
        } 

        public static Snippets Instance {
            get {
                try {
                    if (instance == null) {
                        instance = new Snippets();                        
                        instance.Load("snippets.xml");
                    }
                    return instance;
                }catch(System.IO.FileNotFoundException){
                    return null;
                }
            }
        }
    }
}
