//Component to wrap around the app and available to 
//all the children by using store context hoop

import { PropsWithChildren, createContext, useContext, useState } from "react";
import { Basket } from "../models/basket";

interface StoreContextValue{
    basket: Basket | null;
    setBasket: (basket: Basket) => void;
    removeItem: (productId: number, quantity: number) => void;
}

export const StoreContext = createContext<StoreContextValue | undefined>(undefined);

//store context hook to consume store provider
export function useStoreContext() {
    const context = useContext(StoreContext);
    
    if (context === undefined){
        throw Error('Oops - we do not seem to be inside the provider');
    }
    return context;
}

//same logic as in api
export function StoreProvider({children}: PropsWithChildren<any>){
    const [basket, setBasket] = useState<Basket | null>(null);

    //Creating copy if items and stores it in a variable
    //Finding the index of the product to update
    //Reduces the quantity and removes item if quantity is 0
function removeItem(productId: number, quantity: number){
    if (!basket) return;
    const items = [...basket.items];
    const itemIndex = items.findIndex(i => i.productId === productId);
    if (itemIndex >=0){
        items[itemIndex].quantity -= quantity;
        if (items[itemIndex].quantity === 0) items.splice(itemIndex, 1);
    setBasket(prevState => {
        return {...prevState!, items}
    })
    }
}

return(
    <StoreContext.Provider value={{basket, setBasket, removeItem}}>
        {children}
    </StoreContext.Provider>
)
}