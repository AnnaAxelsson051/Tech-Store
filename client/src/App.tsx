import { useEffect, useState } from "react";


function App() {
  const [products, setProducts] = useState([
    {name: 'product1', price: 100.00},
    {name: 'product2', price: 200.00},
  ]);

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
      {name: 'product' + (prevState.length + 1), price: (prevState.length * 100) + 100}])
  }

  return (
    <div>
    <h1>Store</h1>
    <ul>
    {products.map((item, index) =>(
      <li key={index}>{item.name} - {item.price}</li>
    ))}
    </ul>
    <button onClick={addProduct}>Add Product</button>
    </div>
  );
}

export default App;
