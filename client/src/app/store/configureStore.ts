
import { configureStore } from "@reduxjs/toolkit";
import { counterSlice } from "../../feat/contact/counterSlice";
import { useDispatch, TypedUseSelectorHook, useSelector } from "react-redux";
import { basketSlice } from "../../feat/basket/basketSlice";
import { catalogSlice } from "../../feat/catalog/catalogSlice";

export const store = configureStore({
    reducer: {
        counter: counterSlice.reducer,
        basket: basketSlice.reducer,
        catalog: catalogSlice.reducer
    }
})

export type RootState = ReturnType<typeof store.getState>;  
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;