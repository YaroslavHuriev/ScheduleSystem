import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import axios from "axios";
import { DataGrid } from '@mui/x-data-grid';
import CircularProgress from '@mui/material/CircularProgress';
import { useParams, useNavigate } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import LoadingButton from '@mui/lab/LoadingButton';

const columns = [
    { field: 'group', headerName: 'Група', flex: 1 },
    { field: 'teacher', headerName: 'Викладач', flex: 1 },
    { field: 'room', headerName: 'Аудиторія', flex: 1 },
    { field: 'discipline', headerName: 'Предмет', flex: 1 }
];

export function LessonTable(props) {
    const [rows, setRows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [buttonLoading, setButtonLoading] = useState(false);
    const navigate = useNavigate();
    const params = useParams();
    useEffect(() => {
        axios.get(`scheduleinputdata/${params.id}`).then((response) => {
            setRows(response.data.data);
            setLoading(false)
        })
    }, [])



    let contents = loading
        ? <Box sx={{ display: 'flex' }}>
            <CircularProgress />
        </Box>
        : <Grid container spacing={2} justifyContent="flex-end"
            alignItems="flex-end">
            <Grid item xs={12}>
                <DataGrid
                    sx={{height: 400, mt:4}}
                    rows={rows.map((item, index) => (
                        {
                            id: index,
                            ...item
                        }
                    ))}
                    columns={columns}
                    pageSize={5}
                    rowsPerPageOptions={[5]}
                    disableSelectionOnClick
                    experimentalFeatures={{ newEditingApi: true }}
                    />
            </Grid>
            <Grid item xs={2}>
                <LoadingButton
                    onClick={() => {
                        setButtonLoading(!buttonLoading)
                        navigate('/schedule/97047616-ea54-4a58-8ec5-81fec661ade5')
                        //axios.post('api/schedule', { inputDataId: params.id })
                    }}
                    loading={buttonLoading}
                    loadingPosition="end"
                    variant="contained"
                    sx={{ height: 32 }}
                    style={{ lineHeight: "12px" }}
                >
                    Згенерувати розклад
                </LoadingButton>
                {/*<Button onClick={() => {*/}
                {/*    axios.post('api/schedule', { inputDataId: params.id })*/}
                {/*}} style={{ lineHeight: "12px" }} variant="contained" sx={{height:32}}>Згенерувати розклад</Button>*/}
            </Grid>
        </Grid>;

    return (
        <div>
            {contents}
        </div>
    );
}
