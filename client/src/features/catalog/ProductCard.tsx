import { ListItem, ListItemAvatar, Avatar, ListItemText, Button, Card, CardActions, CardContent, CardMedia, Typography, CardHeader } from "@mui/material";
import { Product } from "../../app/models/product";

interface Props {
    product: Product;
}
export default function ProductCard({product}: Props){
    return(
        <Card>
            <CardHeader
            avatar={
                <Avatar>
{product.name.charAt(0).toUpperCase()}
                </Avatar>
            }
            title={product.name}
                />
        <CardMedia
          sx={{ height: 140, backgroundSize: 'contain' }}
          //image="/static/images/cards/contemplative-reptile.jpg"
          //image="http://picsum.photos/200"
          image={product.pictureUrl}
          //title="green iguana"
          title={product.name}
        />
        <CardContent>
          <Typography gutterBottom color='secondary' variant="h5">
            {/*Lizard*/}
            {product.price}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {product.brand} / {product.type}
            {/*Lizards are a widespread group of squamate reptiles, with over 6,000
            species, ranging across all continents except Antarctica*/}
          </Typography>
        </CardContent>
        <CardActions>
          <Button size="small">Add to cart</Button>
          <Button size="small">View</Button>
        </CardActions>
      </Card>
    )
}