import { ListItem, ListItemAvatar, Avatar, ListItemText, Button, Card, CardActions, CardContent, CardMedia, Typography, CardHeader } from "@mui/material";
import { Product } from "../../app/models/product";
import { useState } from "react";
import agent from "../../app/api/agent"
import { Link } from "react-router-dom";
import { LoadingButton } from "@mui/lab";
import { useStoreContext } from "../context/StoreContext";
import { currencyFormat } from "../../app/util/util";

interface Props {
    product: Product;
}
export default function ProductCard({product}: Props){
const [loading, setLoading] = useState(false);
const {setBasket} = useStoreContext();

function handleAddItem(productId: number){
  setLoading(true);
  agent.Basket.addItem(productId)
  .then(basket => setBasket(basket))
  .catch(error => console.log())
  .finally(() => setLoading(false));
}

    return(
        <Card>
            <CardHeader
            avatar={
                <Avatar sx={{bgcolor: 'secondary.main'}}>
                    {product.name.charAt(0).toUpperCase()} 
                    
                </Avatar>
            }
            title={product.name}
            titleTypographyProps={{
                sx: {fontWeight: 'bold', color: 'primary.main'}
            }}
                />
        <CardMedia
          sx={{ height: 140, backgroundSize: 'contain', bgcolor: 'primary.light' }}
      
          image={product.pictureUrl}
          title={product.name}
        />
        <CardContent>
          <Typography gutterBottom color='secondary' variant="h5">
           {currencyFormat(product.price)}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {product.brand} | Watches{product.type}
            {/* <p>It keeps track of health, motivates to exercise
            and movement, and has innovative safety features
            such as crash and fall detection. With eSIM for your Apple Watch SE (2022)
            you also stay connected without the mobile phone.*/}
        
          </Typography>
        </CardContent>
        <CardActions>
       
          <LoadingButton 
          loading={loading} 
          onClick={() => handleAddItem(product.id)} 
          size="small">Add to cart</LoadingButton>
          <Button component={Link} to={`/catalog/${product.id}`} size="small">View</Button>
        </CardActions>
      </Card>

      
    )
}