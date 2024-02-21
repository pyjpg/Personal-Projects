import { TextField, debounce } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setProductParams } from "./catalogSlice";
import { useState } from "react";

export default function ProductSearch() {
    const {ProductParams} = useAppSelector(state => state.catalog);
    const [searchTerm, setSearchTerm] = useState(ProductParams.searchTerm);
    const dispatch = useAppDispatch();
    const debouncedSearch =  debounce((event: any) => {
        dispatch(setProductParams({searchTerm: event.target.value}))
    }, 1000)
    return (
        <TextField
            label='Search products'
            variant='outlined'
            fullWidth
            value={searchTerm || ''}
            onChange={(event: any) => {
                setSearchTerm(event.target.value);
                debouncedSearch(event);
            }}
        />
    )
}