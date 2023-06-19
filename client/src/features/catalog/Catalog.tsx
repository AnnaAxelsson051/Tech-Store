
import { useState, useEffect } from "react";
import ProductList from "./ProductList";
import { Product } from "../../app/models/product";


export default function Catalog(){
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
      fetch('http://localhost:5152/api/products')
      .then(response => response.json())
      .then(data => setProducts(data))
    }, [])
  
    return (
        <>
       <ProductList products={products}/>
        </>
    )
        }