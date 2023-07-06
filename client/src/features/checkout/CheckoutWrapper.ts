import { Elements } from "@stripe/react-stripe-js";
import CheckoutPage from "./CheckoutPage";
import { loadStripe } from "@stripe/stripe-js";

const stripePromise = loadStripe("pk_test_51NQsSoBUIbQYd7DMQUv3fo7dAEm7BgC5BEnckdoz2IIaq1oG4GeHjQYsBlVySU2wtmlAErkfnqoEUP6SrJ9L2yVR00vrsxYuNW")

export default function CheckoutWrapper(){
    return(
        <Elements stripe={stripePromise}>
           <CheckoutPage/>
        </Elements>

    )
}

