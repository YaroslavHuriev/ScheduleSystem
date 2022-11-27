import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import CircularProgress from '@mui/material/CircularProgress';
import { useEffect, useState, Fragment } from 'react';
import axios from 'axios';

export function AsyncSearchSelect(props) {
    const [open, setOpen] = useState(false);
    const [options, setOptions] = useState([]);
    const [loading, setLoading] = useState(false);

    const handleInputChange = (e) => {
        setLoading(true)
        axios.get(props.dataPath + `?searchString=${e.target.value}`).then((response) => {
            setOptions(response.data)
            setLoading(false)
        })
    }

    useEffect(() => {
        if (!open) {
            setOptions([]);
        }
    }, [open]);

    return (
        <Autocomplete
            id="asynchronous-demo"
            open={open}
            name={props.name}
            filterOptions={(x) => x}
            onOpen={() => {
                setOpen(true);
            }}
            onClose={() => {
                setOpen(false);
            }}
            isOptionEqualToValue={(option, value) => option[props.optionName] === value[props.optionName]}
            getOptionLabel={(option) => option[props.optionName]}
            options={options}
            onChange={(e, v) => props.onChange({ target: { name: props.name, value: v ? v.id : '' } })}
            loading={loading}
            renderInput={(params) => (
                <TextField
                    {...params}
                    margin="normal"
                    onChange={handleInputChange}
                    label={props.label}
                    InputProps={{
                        ...params.InputProps,
                        endAdornment: (
                            <Fragment>
                                {loading ? <CircularProgress color="inherit" size={20} /> : null}
                                {params.InputProps.endAdornment}
                            </Fragment>
                        ),
                    }}
                />
            )}
        />
    );
}