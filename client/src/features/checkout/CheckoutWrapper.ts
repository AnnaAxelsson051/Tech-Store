import { Elements } from "@stripe/react-stripe-js";
import CheckoutPage from "./CheckoutPage";
import { loadStripe } from "@stripe/stripe-js";
import { useAppDispatch } from "../../app/store/configureStore";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import { setBasket } from "../basket/BasketSlice";

const stripePromise = loadStripe("pk_test_51NQsSoBUIbQYd7DMQUv3fo7dAEm7BgC5BEnckdoz2IIaq1oG4GeHjQYsBlVySU2wtmlAErkfnqoEUP6SrJ9L2yVR00vrsxYuNW")

export default function CheckoutWrapper(){
const dispatch = useAppDispatch();
const [loading, setLoading] = useState(true);

useEffect(() => {
agent.Payments.createPaymentIntent()
.then(basket => dispatch(setBasket(basket)))
.catch(error => console.log(error))
.finally(() => setLoading(false))
}, [dispatch]);

if (loading) return <LoadingComponent message='Looading checkout...'/>

    return (
        <Elements stripe={stripePromise}>
           <CheckoutPage/>
        </Elements>

    )
}

