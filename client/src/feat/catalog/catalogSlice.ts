import { createAsyncThunk, createEntityAdapter, createSlice, isAction } from "@reduxjs/toolkit";
import { Product, ProductParams } from "../../app/models/product";
import agent from "../../app/api/agent";
import { store } from "../../app/store/configureStore";

interface CatalogState {
    productsLoaded:boolean;
    filtersLoaded: boolean;
    status: string;
    brands: string[];
    types: string[];
    ProductParams: ProductParams;
}

const productsAdapter = createEntityAdapter<Product>();

function getAxiosParams(ProductParams: ProductParams) {
    const params = new URLSearchParams();
    params.append('pageNumber', ProductParams.pageNumber.toString());
    params.append('pageSize', ProductParams.pageSize.toString());
    params.append('orderBy', ProductParams.orderBy);
    if (ProductParams.searchTerm) params.append('searchTerm', ProductParams.searchTerm);
    if (ProductParams.brands) params.append('brands', ProductParams.brands.toString());
    if (ProductParams.types) params.append('brands', ProductParams.types.toString());
    return params;
}

export const fetchProductsAsync = createAsyncThunk<Product[], void, {state: RootState}>(
    'catalog/fetchProductsAsync',
    async (_, thunkAPI) => {
        const params = getAxiosParams(thunkAPI.getState().catalog.ProductParams);
        try{
            return await agent.Catalog.list(params);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
)

export const fetchProductAsync = createAsyncThunk<Product, number>(
    'catalog/fetchProductAsync',
    async (productId, thunkAPI) => {
        try{
            const product = await agent.Catalog.details(productId);
            return product;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({error: error.data});
        }
    }
)

export const fetchFilters = createAsyncThunk(
    'catalog/fetchFilters',
    async (_, thunkAPI) => {
        try{
            return agent.Catalog.fetchFilters();
        } catch (error : any)
        {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
)

function InitParams()
{
    return {
            pageNumber: 1,
            pageSize: 6,
            orderBy: 'name'
    }
}

export const catalogSlice = createSlice({
    name: 'catalog',
    initialState: productsAdapter.getInitialState<CatalogState>({
        productsLoaded: false,
        filtersLoaded: false,
        status: 'idle',
        brands: [],
        types: [],
        ProductParams: InitParams()
    }),
    reducers: {
        setProductParams: (state, action) => {
            state.productsLoaded = false;
            state.ProductParams = {...state.ProductParams, ...action.payload};
        },
        resetProductParms: (state) => {
            state.ProductParams = InitParams();
        }
    },
    extraReducers: (builder => {
        builder.addCase(fetchProductsAsync.pending, (state) => {
            state.status = 'pendingFetchProducts';
        });
        builder.addCase(fetchProductsAsync.fulfilled, (state, action) =>{
            productsAdapter.setAll(state, action.payload);
            state.status = 'idle';
            state.productsLoaded = true;
        });
        builder.addCase(fetchProductsAsync.rejected, (state, action) => {
            console.log(action.payload);
            state.status = 'idle';
        });
        builder.addCase(fetchProductAsync.pending, (state) => {
            state.status = 'pendingFetchProduct'
        });
        builder.addCase(fetchProductAsync.fulfilled, (state, action) => {
            productsAdapter.upsertOne(state, action.payload);
            state.status = 'idle';
        });
        builder.addCase(fetchProductAsync.rejected, (state,action) => {
            console.log(action);
            state.status = 'idle';
            
        });
        builder.addCase(fetchFilters.pending, (state) => {
            state.status = 'pendingFetchFilters';
        });
        builder.addCase(fetchFilters.fulfilled, (state, action) => {
            state.brands = action.payload.brands;
            state.types = action.payload.types;
            state.filtersLoaded = true;
            state.status = 'idle';
        });
        builder.addCase(fetchFilters.rejected, (state, action) => {
            state.status = 'idle';
            console.log(action.payload);
        })
    })
})


type RootState = ReturnType<typeof store.getState>
 
export const productSelectors = productsAdapter.getSelectors((state: RootState) => state.catalog);

export const {setProductParams, resetProductParms} = catalogSlice.actions;

