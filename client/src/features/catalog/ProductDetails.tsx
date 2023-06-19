import { Typography } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Product } from "../../app/models/product";

//Product having initial value of null and if it does not exist
export default function ProductDetails(){
const {id} = useParams<{id: string}>();
const [product, setProduct] = useState<Product | null>(null);
const [loading, setLoading] = useState(true);

//Getting the specific product
useEffect(() => {
    axios.get(`http://localhost:5152/api/products${id}`)
         .then(response => setProduct(response.data))
         .catch(error => console.log(error))
         .finally(() => setLoading(false));
        }, [id])

    return(
        <Typography variant="h2">
            Product details
        </Typography>
    )
}