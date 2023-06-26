import { Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, TextField, Typography } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync, removeBasketItemAsync} from "../basket/BasketSlice";
import { fetchProductAsync, productSelectors } from "./catalogSlice";

//displays and manages the details of a specific product

//ProductDetails extracts data from the Redux store using the useAppSelector hook and initializes 
//various state variables using the useState hook
export default function ProductDetails() {
    const {basket, status} = useAppSelector(state => state.basket);
    const dispatch = useAppDispatch();
    const { id } = useParams<{ id: string }>();
    const product = useAppSelector(state => productSelectors.selectById(state, id));
    const {status: productStatus} = useAppSelector(state => state.catalog);
    const [quantity, setQuantity] = useState(0);
    const item = basket?.items.find(i => i.productId === product?.id);

    //Getting the specific product
    useEffect(() => {
        if (item) setQuantity(item.quantity);
        if (!product && id) dispatch(fetchProductAsync(parseInt(id)));
    }, [id, item, dispatch, product])

    // handling input change events and updating the quantity
    function handleInputChange(event: any){
        if(event.target.value >= 0){
        setQuantity(parseInt(event.target.value));
        }
    }

    // handles the logic for updating the shopping cart based on the 
    //current quantity and item details.
    function handleUpdateCart(){
         if (!item || quantity > item.quantity){
            const updatedQuantity = item ? quantity - item.quantity : quantity;
            dispatch(addBasketItemAsync({productId: product?.id!, quantity: updatedQuantity})) 
        }else{
            const updatedQuantity = item.quantity - quantity;
            dispatch(removeBasketItemAsync({productId: product?.id!, quantity: updatedQuantity}))
         }
    }

    if (productStatus.includes('pending')) return <LoadingComponent message='Loading product...'/>
    if (!product) return <NotFound/>

    return (
        <Grid container spacing={6}>
            <Grid item xs={6}>
                <img src={product.pictureUrl} alt={product.name} style={{ width: '100%' }} />
            </Grid>
            <Grid item xs={6}>
                <Typography variant='h3'>{product.name}</Typography>
                <Divider sx={{mb: 2}}/>
                <Typography variant='h4' color='secondary'>${(product.price / 100).toFixed(2)}</Typography>
            <TableContainer>
                <Table>
                    <TableBody>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>{product.name}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Description</TableCell>
                            <TableCell>{product.description}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Type</TableCell>
                            <TableCell>{product.type}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Brand</TableCell>
                            <TableCell>{product.brand}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Quantity in stock</TableCell>
                            <TableCell>{product.quantityInStock}</TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
            <Grid container spacing={2}>
                <Grid item xs={6}>
                    <TextField
                    onChange={handleInputChange}
                    variant='outlined'
                    type='number'
                    label='Quantity in cart'
                    fullWidth
                    value={quantity}
                    />
                </Grid>
                <Grid item xs={6}>
                <LoadingButton
                disabled={item?.quantity === quantity || !item && quantity === 0}
                loading={status.includes('pending')}
                onClick={handleUpdateCart}
                sx={{height: '55px'}}
                color='primary'
                size='large'
                variant='contained'
                fullWidth
                >
                    {item ? 'Update Quantity' : 'Add to cart'}
                </LoadingButton>
                </Grid>
            </Grid>
            </Grid>
        </Grid>
    )
}