import { ListItem, ListItemAvatar, Avatar, ListItemText, Button, Card, CardActions, CardContent, CardMedia, Typography, CardHeader, Link } from "@mui/material";
import { Product } from "../../app/models/product";

interface Props {
    product: Product;
}
export default function ProductCard({product}: Props){
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
            ${(product.price / 100).toFixed(2)}
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
       
          <Button size="small">Add to cart</Button>
          <Button component={Link} to={`/catalog/${product.id}`} size="small">View</Button>
        </CardActions>
      </Card>

      
    )
}