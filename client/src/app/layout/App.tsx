import { useEffect, useState } from "react";
import { Product } from "../models/product";
import Catalog from "../../features/catalog/Catalog";


function App() {
  const [products, setProducts] = useState<Product[]>([]);

  //Adds side effect to component when it loads 
  //Fetching products from api, extracting a json body 
  //setting the json body as the value of the products variable
  useEffect(() => {
    fetch('http://localhost:5152/api/products')
    .then(response => response.json())
    .then(data => setProducts(data))
  }, [])

  function addProduct(){
    setProducts(prevState => [...prevState, 
      {
        id: prevState.length +101,
        name: 'product' + (prevState.length + 1), 
        price: (prevState.length * 100) + 100,
        brand: 'some brand',
        description: 'some description',
        pictureUrl: 'http://picsum.photos/200'
      }])
  }

  return (
    <div>
    <h1>Store</h1>
    <Catalog products={products} addProduct={addProduct}/>
   
    </div>
  );
}

export default App;
