import { Grid, List } from "@mui/material";
import { Product } from "../../app/models/product";
import ProductCard from "./ProductCard";


interface Props {
    products: Product[];
}

export default function ProductList({products}: Props){
    return (
    <Grid container spacing={4}>
    {products.map(product =>(
        //6 = two products on each row
        //4 = three products on each row
        <Grid item xs={6} key={product.id}>
  <ProductCard product ={product}/>
  </Grid>
    ))}
    </Grid>
    )
}