import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import axios from "axios";
import { DataGrid } from '@mui/x-data-grid';
import CircularProgress from '@mui/material/CircularProgress';
import { useNavigate } from "react-router-dom";

const columns = [
    { field: 'name', headerName: 'Name', flex: 1 },
    { field: 'id', headerName: 'ID', flex: 1 }
];

export function FetchData(props) {
    const [rows, setRows] = useState([]);
    const [loading, setLoading] = useState(true);

    const navigate = useNavigate();
    useEffect(() => {
        axios.get('scheduleinputdata').then((response) => {
            setRows(response.data);
            setLoading(false)
        })
    }, [])



    let contents = loading
        ? <Box sx={{ display: 'flex' }}>
            <CircularProgress />
        </Box>
        : <Box sx={{ height: 400, width: '100%', mt:4 }}>
            <DataGrid
                rows={rows}
                columns={columns}
                pageSize={5}
                rowsPerPageOptions={[5]}
                checkboxSelection
                disableSelectionOnClick
                experimentalFeatures={{ newEditingApi: true }}
                onRowClick={(params) => navigate(`/scheduleinputdata/${params.id}`)}
            />
        </Box>;

    return (
        <div>
            {contents}
        </div>
    );
}
