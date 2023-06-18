import { Product } from "../../app/models/product";
import { List, ListItem, ListItemAvatar, Avatar, ListItemText, Button } from "@mui/material";

interface Props{
    products: Product[];
    addProduct: () => void;
}

export default function Catalog({products, addProduct}: Props){
    return (
       <List>
        <List>
        {products.map(product =>(
         <ListItem key={product.name}> 
         <ListItemAvatar>
            <Avatar src={product.pictureUrl}/>
         </ListItemAvatar>
         <ListItemText>
            {product.name} - {product.price}
         </ListItemText>
         </ListItem>
        ))}
        </List>
        <Button variant="contained" onClick={addProduct}>Add Product</Button>
        </List>
    )
        }