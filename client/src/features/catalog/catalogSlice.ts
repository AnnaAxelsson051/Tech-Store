import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { Product } from "../../app/models/product";
import agent from "../../app/api/agent";

const productsAdapter = createEntityAdapter <Product>();

    export const fetchProductAsync = createAsyncThunk<Product[]>(
    'catalog/fetchProductsAsync',
    async () => {
        try {
return await agent.Catalog.list();
        } catch (error){
            console.log(error);
        }
    }
)

export const catalogSlice = createSlice ({
    name: 'catalog',
    initialState: productsAdapter.getInitialState({
        productsLoaded: false,
        status: 'idle'
    }),
    reducers: {},
    extraReducers: (builder => {
        builder.addCase(fetchProductAsync.pending, (state) =>{
            state.status = 'pendingFetchProducts';
        })
    })
})