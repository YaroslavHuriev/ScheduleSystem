import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import axios from './AxiosInterceptor'
import { EditingState } from '@devexpress/dx-react-grid';
import {
    PagingState,
    IntegratedPaging,
} from '@devexpress/dx-react-grid';
import {
    Grid,
    Table,
    TableHeaderRow,
    TableEditColumn,
    PagingPanel,
} from '@devexpress/dx-react-grid-material-ui';
import CircularProgress from '@mui/material/CircularProgress';
import { useParams, useNavigate } from 'react-router-dom';
import { DialogContentText, Grid as MUIGrid } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import TextField from '@mui/material/TextField';
import FormGroup from '@mui/material/FormGroup';
import Button from '@mui/material/Button';
import {
    Plugin, Template, TemplateConnector, TemplatePlaceholder,
} from '@devexpress/dx-react-core';
import { AsyncSearchSelect } from './AsyncSearchSelect';
import { AsyncSearchTeacherSelect } from './AsyncSearchTeacherSelect';
import { ErrorSnackBar } from './ErrorSnackbar';

const columns = [
    { name: 'group', title: 'Група' },
    { name: 'teacher', title: 'Викладач' },
    { name: 'room', title: 'Аудиторія' },
    { name: 'discipline', title: 'Предмет' }
];

const getRowId = row => row.id;

const Popup = ({
    row,
    onChange,
    onApplyChanges,
    onCancelChanges,
    open,
}) => (
    <Dialog scroll='body' fullWidth={true} open={open} onClose={onCancelChanges} aria-labelledby="form-dialog-title">
        <DialogTitle id="form-dialog-title">Деталі заняття</DialogTitle>
        <DialogContent>
            <MUIGrid container spacing={3}>
                <MUIGrid item xs={6}>
                    <FormGroup>
                        <AsyncSearchTeacherSelect
                            onChange={onChange}
                        />
                        <TextField
                            fullWidth
                            margin="normal"
                            name="room"
                            label="Аудиторія"
                            value={row.room || ''}
                            onChange={onChange}
                        />
                    </FormGroup>
                </MUIGrid>
                <MUIGrid item xs={6}>
                    <FormGroup>
                        <AsyncSearchSelect
                            dataPath='api/groups'
                            optionName='name'
                            name="group"
                            label="Група"
                            onChange={onChange}
                        />
                        <TextField
                            fullWidth
                            margin="normal"
                            name="discipline"
                            label="Дисципліна"
                            value={row.discipline || ''}
                            onChange={onChange}
                        />
                    </FormGroup>
                </MUIGrid>
            </MUIGrid>
        </DialogContent>
        <DialogActions>
            <Button variant="outlined" onClick={onCancelChanges}>
                Відмінити
            </Button>
            <Button variant="contained" onClick={onApplyChanges}>
                Зберегти
            </Button>
        </DialogActions>
    </Dialog>
);

const PopupEditing = React.memo(({ popupComponent: Popup }) => (
    <Plugin>
        <Template name="popupEditing">
            <TemplateConnector>
                {(
                    {
                        rows,
                        getRowId,
                        addedRows,
                        editingRowIds,
                        createRowChange,
                        rowChanges,
                    },
                    {
                        changeRow, changeAddedRow, commitChangedRows, commitAddedRows,
                        stopEditRows, cancelAddedRows, cancelChangedRows,
                    },
                ) => {
                    const isNew = addedRows.length > 0;
                    let editedRow;
                    let rowId;
                    if (isNew) {
                        rowId = 0;
                        editedRow = addedRows[rowId];
                    } else {
                        [rowId] = editingRowIds;
                        const targetRow = rows.filter(row => getRowId(row) === rowId)[0];
                        editedRow = { ...targetRow, ...rowChanges[rowId] };
                    }

                    const processValueChange = ({ target: { name, value } }) => {
                        const changeArgs = {
                            rowId,
                            change: createRowChange(editedRow, value, name),
                        };
                        if (isNew) {
                            changeAddedRow(changeArgs);
                        } else {
                            changeRow(changeArgs);
                        }
                    };
                    const rowIds = isNew ? [0] : editingRowIds;
                    const applyChanges = () => {
                        if (isNew) {
                            commitAddedRows({ rowIds });
                        } else {
                            stopEditRows({ rowIds });
                            commitChangedRows({ rowIds });
                        }
                    };
                    const cancelChanges = () => {
                        if (isNew) {
                            cancelAddedRows({ rowIds });
                        } else {
                            stopEditRows({ rowIds });
                            cancelChangedRows({ rowIds });
                        }
                    };

                    const open = editingRowIds.length > 0 || isNew;
                    return (
                        <Popup
                            open={open}
                            row={editedRow}
                            onChange={processValueChange}
                            onApplyChanges={applyChanges}
                            onCancelChanges={cancelChanges}
                        />
                    );
                }}
            </TemplateConnector>
        </Template>
        <Template name="root">
            <TemplatePlaceholder />
            <TemplatePlaceholder name="popupEditing" />
        </Template>
    </Plugin>
));



export function LessonTable(props) {
    const [openGenerateDialog, setOpenGenerateDialog] = useState(false);
    const [scheduleName, setScheduleName] = useState('');
    const [rows, setRows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [buttonLoading, setButtonLoading] = useState(false);
    const navigate = useNavigate();
    const params = useParams();
    const handleGenerateDialogClose = () => {
        setOpenGenerateDialog(false);
      };
    useEffect(() => {
        axios.get(`api/lessons?inputDataId=${params.id}`).then((response) => {
            setRows(response.data);
            setLoading(false)
        })
    }, [])

    const [snackbarState, setSnackbarState] = React.useState({
        open: false,
        message: ''
    });

    const handleSnackbarClose = (event, reason) => {
        setSnackbarState({
            ...snackbarState,
            open: false
        });
    };

    const commitChanges = ({ added, changed }) => {
        let changedRows;
        if (added) {
            axios.post('api/lessons', {
                teacherId: added[0].teacher,
                groupId: added[0].group,
                discipline: added[0].discipline,
                room: added[0].room,
                inputDataId: params.id
            }).then(response => {
                axios.get(`api/lessons/${response.data}`).then(result => {
                    changedRows = [
                        ...rows,
                        ...[result.data],
                    ];
                    setRows(changedRows);
                })
            })
        }
        if (changed) {
            changedRows = rows.map(row => (changed[row.id] ? { ...row, ...changed[row.id] } : row));
        }
    };

    let contents = loading
        ? <Box sx={{ display: 'flex' }}>
            <CircularProgress />
        </Box>
        : <MUIGrid container spacing={2} justifyContent="flex-end"
            alignItems="flex-end">
            <MUIGrid item xs={12}>
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
                    <Table messages={{ noData: 'Немає даних' }}
                    />
                    <TableHeaderRow />
                    {/* <TableEditRow /> */}
                    <TableEditColumn
                        messages={{ addCommand: 'Додати', deleteCommand: 'Видалити' }}
                        showAddCommand
                        showDeleteCommand
                    />
                    <PopupEditing popupComponent={Popup} />
                    <PagingPanel messages={{ info: (parameters) => `${parameters.from}-${parameters.to} із ${parameters.count}` }} />
                </Grid>
            </MUIGrid>
            <MUIGrid item xs={2}>
                <LoadingButton
                    onClick={() => {
                        if (!rows || rows.length === 0) {
                            setSnackbarState({ open: true, message: 'Помилка: вхідні дані порожні.' })
                            return;
                        }
                        setOpenGenerateDialog(true);
                        // setButtonLoading(!buttonLoading)
                        // axios.post('api/schedules', { inputDataId: params.id }).then((response) => {
                        //     navigate(`/schedule/${response.data}`)
                        // })
                    }}
                    loading={buttonLoading}
                    loadingPosition="end"
                    variant="contained"
                    sx={{ height: 32 }}
                    style={{ lineHeight: "12px" }}
                >
                    Згенерувати розклад
                </LoadingButton>
            </MUIGrid>
            <ErrorSnackBar
                open={snackbarState.open}
                message={snackbarState.message}
                handleClose={handleSnackbarClose} />
            <Dialog open={openGenerateDialog} onClose={handleGenerateDialogClose}>
                <DialogTitle>Генерація розкладу</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Будь ласка введіть назву для нового розкладу:
                    </DialogContentText>
                    <TextField
                        autoFocus
                        onChange={(event) => setScheduleName(event.target.value)}
                        margin="dense"
                        id="name"
                        value={scheduleName}
                        label="Назва розкладу"
                        fullWidth
                        variant="standard"
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleGenerateDialogClose}>Відмінити</Button>
                    <Button variant='contained' onClick={() => {
                        setOpenGenerateDialog(false)
                        setButtonLoading(!buttonLoading)
                        axios.post('api/schedules', { inputDataId: params.id, name: scheduleName }).then((response) => {
                            navigate(`/schedule/${response.data}`)
                        })
                    }}>Згенерувати</Button>
                </DialogActions>
            </Dialog>
        </MUIGrid>;

    return (
        <div>
            {contents}
        </div>
    );
}
