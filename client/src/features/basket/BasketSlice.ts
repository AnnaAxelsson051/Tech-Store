import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit"
import { Basket } from "../../app/models/basket"
import { getCookie } from "../../app/util/util";
import agent from "../../app/api/agent";

//defines the shape of the state object for the basket slice
interface BasketState {
    basket: Basket | null;
    status: string;
}

//It initializes the initial state for the basket slice
const initialState: BasketState = {
    basket: null,
    status: 'idle'
}

export const fetchBasketAsync = createAsyncThunk<Basket>(
    'basket/fetchBasketAsync',
    async (_, thunkAPI) => {
        try {
            return await agent.Basket.get();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    },
    {
        condition: () => {
            if (!getCookie('buyerId')) return false;
        }
    }
)

// asynchronous action creator: It is created using the createAsyncThunk function provided
// by Redux Toolkit and handles the process of 
//adding an item to the basket asynchronously.
export const addBasketItemAsync = createAsyncThunk<Basket, { productId: number, quantity?: number }>(
    'basket/addBasketItemAsync',
    async ({ productId, quantity = 1 }, thunkAPI) => {
        try {
            return await agent.Basket.addItem(productId, quantity);
        } catch (error) {
            return thunkAPI.rejectWithValue({ error: error.data })
        }
    }
)

export const removeBasketItemAsync = createAsyncThunk<void,
    { productId: number, quantity: number, name?: string }>(
        'basket/removeBasketItemAsync',
        async ({ productId, quantity }, thunkAPI) => {
            try {
                await agent.Basket.removeItem(productId, quantity);
            } catch (error: any) {
                return thunkAPI.rejectWithValue({ error: error.data })
            }

        }
    )

//Creating a basket slice
export const basketSlice = createSlice({
    name: 'basket',
    initialState,
    reducers: {
        setBasket: (state, action) => {
            state.basket = action.payload
        }
    },

    //The code handles actions related to adding and 
    //removing items from a basket asynchronously. 
    extraReducers: (builder => {
        builder.addCase(addBasketItemAsync.pending, (state, action) => {
            state.status = 'pendingAddItem' + action.meta.arg.productId;
        });
   
        builder.addCase(removeBasketItemAsync.pending, (state, action) => {
            state.status = 'pendingRemoveItem' + action.meta.arg.productId + action.meta.arg.name;
        });
        builder.addCase(removeBasketItemAsync.fulfilled, (state, action) => {
            const { productId, quantity } = action.meta.arg;
            const itemIndex = state.basket?.items.findIndex(i = i.productId === productId);
            if (itemIndex === -1 || itemIndex == undefined) return;
            state.basket!.items[itemIndex].quantity -= quantity!;
            if (state.basket?.items[itemIndex].quantity === 0)
                state.basket.items.splice(itemIndex, 1);
            state.status = 'idle';
        });
        builder.addCase(removeBasketItemAsync.rejected, (state, action) => {
            state.status = 'idle';
            console.log(action.payload);
        });
        builder.addMatcher(isAnyOf(addBasketItemAsync.fulfilled, fetchBasketAsync.fulfilled), (state, action) => {
            state.basket = action.payload;
            state.status = 'idle';
        });
        builder.addMatcher(isAnyOf(addBasketItemAsync.rejected, fetchBasketAsync.rejected), (state, action) => {
            state.status = 'idle';
            console.log(action.payload);
        });
    })
})

export const { setBasket } = basketSlice.actions;