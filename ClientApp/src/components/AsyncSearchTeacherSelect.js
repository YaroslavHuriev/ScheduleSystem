import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import CircularProgress from '@mui/material/CircularProgress';
import { useEffect, useState, Fragment } from 'react';
import axios from './AxiosInterceptor'

export function AsyncSearchTeacherSelect(props) {
    const [open, setOpen] = useState(false);
    const [options, setOptions] = useState([]);
    const [loading, setLoading] = useState(false);

    const handleInputChange = (e) => {
        setLoading(true)
        axios.get(`api/teachers?searchString=${e.target.value}`).then((response) => {
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
            name='teacher'
            filterOptions={(x) => x}
            onOpen={() => {
                setOpen(true);
            }}
            onClose={() => {
                setOpen(false);
            }}
            isOptionEqualToValue={(option, value) => option.surname + option.firstName === value.surname + value.firstName}
            getOptionLabel={(option) => option.surname + ' ' + option.firstName}
            options={options}
            onChange={(e, v) => props.onChange({ target: { name: 'teacher', value: v ? v.id : '' } })}
            loading={loading}
            renderInput={(params) => (
                <TextField
                    {...params}
                    margin="normal"
                    onChange={handleInputChange}
                    label='Викладач'
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