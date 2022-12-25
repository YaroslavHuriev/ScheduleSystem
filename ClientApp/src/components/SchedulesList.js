import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import { EditingState } from '@devexpress/dx-react-grid';
import CircularProgress from '@mui/material/CircularProgress';
import { useNavigate } from "react-router-dom";
import {
    PagingState,
    IntegratedPaging,
} from '@devexpress/dx-react-grid';
import {
    Grid,
    Table,
    TableHeaderRow,
    TableEditRow,
    TableEditColumn,
    PagingPanel,
} from '@devexpress/dx-react-grid-material-ui';
import axios from './AxiosInterceptor'
import { SuccessSnackBar } from './SuccessSnackBar';
import { TableRow } from './TableRow';

const editColumnMessages = {
    addCommand: 'Додати',
    editCommand: 'Редагувати',
    deleteCommand: 'Видалити',
    commitCommand: 'Зберегти',
    cancelCommand: 'Відмінити',
};

const columns = [
    { name: 'name', title: 'Назва' }
];

const getRowId = row => row.id;

export function SchedulesList(props) {
    const [rows, setRows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [snackbarState, setSnackbarState] = useState({
        open: false,
        message: ''
    });

    const navigate = useNavigate();
    useEffect(() => {
        console.log(axios.defaults.headers.common["Authorization"])
        axios.get('api/schedules').then((response) => {
            setRows(response.data);
            setLoading(false)
        })
    }, [])

    const handleSnackbarClose = (event, reason) => {
        setSnackbarState({
            ...snackbarState,
            open: false
        });
    };

    const Row = ({ row, ...restProps }) => (
        <TableRow
            {...restProps}
            onClick={() =>navigate(`/schedule/${row.id}`)}
        />
    );

    const commitChanges = ({ added, changed, deleted }) => {
        let changedRows;
        if (deleted) {
            axios.delete(`api/schedules/${deleted[0]}`).then((response) => {
                setSnackbarState({ open: true, message: 'Розклад успішно видалений!' })
                const deletedSet = new Set(deleted);
                changedRows = rows.filter(row => !deletedSet.has(row.id));
                setRows(changedRows);
            })
        }
    };



    let contents = loading
        ? <Box sx={{ display: 'flex' }}>
            <CircularProgress />
        </Box>
        : <Box sx={{ height: 400, width: '100%', mt: 4 }}>
            <Grid
                rows={rows}
                columns={columns}
                getRowId={getRowId}
            >
                <PagingState
                    defaultCurrentPage={0}
                    pageSize={5}
                />
                
                <IntegratedPaging />
                <EditingState
                    onCommitChanges={commitChanges}
                />
                <Table rowComponent={Row} />
                <TableHeaderRow />
                <TableEditRow />
                <TableEditColumn
                    showDeleteCommand={localStorage.getItem("username") === "admin"}
                    messages={editColumnMessages}
                />
                <PagingPanel />
            </Grid>
        </Box>;

    return (
        <div>
            {contents}
        </div>
    );
}
