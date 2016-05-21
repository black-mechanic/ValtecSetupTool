#include "drivers/drv_user.h"

#define MYDEVICE_BUF_SIZE_TX	8
#define CMD_LENGTH	5
#define MYDEVICE_BUF_SIZE_RX	200
#define ANSW_LENGTH	150
#define MYDEVICE_TIMEOUT	2000

#ifndef min
#define min(a, b) (a)>(b)?(b):(a)
#endif

typedef struct MyDeviceCB
{
	// ������ ��� ������� �������, ����� ������ �� COM �����
	char SendBuf[MYDEVICE_BUF_SIZE_TX]; 
	char RecvBuf[MYDEVICE_BUF_SIZE_RX];
	// ������� �������� ��� ����������� �����
	RFLOAT PTR		pFloat;
	RBOOLEAN PTR	pBool;
	//������� ��� �������� �������
	DWORD PTR		ids;
} MyDeviceCB;

enum MyDeviceGroupID
{
	MyDeviceGroupAO			= 1,	// ���������� ���������
	MyDeviceGroupDO			= 2,	// ������� ������������ � ������� 
	MyDeviceGroupAI			= 3,	// ���������� �� ����� ��������
	MyDeviceGroupDI			= 4,	// ���������� �� ����� �����
	MyDeviceGroupArchiveAI	= 5,
	MyDeviceGroupArchiveDI	= 6,
	MyDevicePinAOG_1	= 100
};


int Driver_MBus(DRIVER_MODE mode, SERIAL_TASK_CB PTR pTaskCB)
{
	if (mode == dmInit){
		MyDeviceCB PTR pCB;
		int index, countA = 0, countD = 0, maxCount = 0;
		
		// ��������� ������
		pTaskCB->pDriverCB = GetMem(sizeof(MyDeviceCB));
		if (!pTaskCB->pDriverCB){
			PRINT0("Driver_MBus. mem allocation error");CR;
			return E_FAIL;
		}
		pCB = (MyDeviceCB PTR)pTaskCB->pDriverCB;

		// ������� ������ ���������� ���������� AO � DO
		for (index = 0 ; index < pTaskCB->QuanModules; ++index){
			DRV_MODULE PTR pModule = GetSerialModule(pTaskCB, index);
			int c;
			c = GetParamCountInGroup(pModule, MyDeviceGroupAO);
			if (c > maxCount){
				maxCount = c;
			}
			countA += c;
			c = GetParamCountInGroup(pModule, MyDeviceGroupDO);
			if (c > maxCount){
				maxCount = c;
			}			
			countD += c;
		}
		// ��������� ������ ��� �������������� �������� ��������� � �����
		pCB->pFloat = GetMem(sizeof(RFLOAT)   * countA);
		if (!pCB->pFloat){
			PRINT0("Driver_MBus. mem allocation error");CR;
			FreeMem (pTaskCB->pDriverCB);
			return E_FAIL;
		}
		pCB->pBool  = GetMem(sizeof(RBOOLEAN) * countD);
		if (!pCB->pBool){
			PRINT0("Driver_MBus. mem allocation error");CR;
			FreeMem (pTaskCB->pDriverCB);
			FreeMem (pCB->pFloat);
			return E_FAIL;
		}
		pCB->ids	= GetMem(sizeof(DWORD)	  * maxCount);
		if (!pCB->ids){
			PRINT0("Driver_MBus. mem allocation error");CR;
			FreeMem (pCB->pFloat);
			FreeMem (pCB->pBool);
			FreeMem (pTaskCB->pDriverCB);
			return E_FAIL;
		}
		//�������������
		pCB->SendBuf[0] = 0x10;
		pCB->SendBuf[1] = 0x5B;
		pCB->SendBuf[4] = 0x16;
		
		for (index = 0 ;index < countA; ++index){
			pCB->pFloat[index] = index;
		}
		for (index = 0; index < countD; ++index){
			pCB->pBool[index] = (index%2)?RTRUE:RFALSE;
		}
		return S_OK;
	}
// ������� ���� ������ ���������� � ������ � ������
	if (mode == dmRead){
		unsigned char uchmBusAddress; // ����� m-Bus ����������, 1 ����, ��� ������� � ������.
		RPARAMPtr pParam;	// ��������� �� ��������� ��������� ����������� �� ���� ������
		RINTEGER imBusAddress; // ����� ��� ������. ����������� ��� �������� �� ����� ������
		RBOOLEAN startFlag; // ����������� ����. ���� =1, �� �������� ���� ������ com-����� ����� ���������� ����
		int bytesRx; // ���������� ���� ���������� �� com-�����
		int timeOutCycles;
		HRESULT checkres;
		RFLOAT fTmp;
		RINTEGER opSerialNumb, opMegaCalories, opVolume, opFTemperature;
		RINTEGER opRTemperature;
		RINTEGER opKiloCalories;
		RINTEGER opVolFlow;
		RINTEGER opWorkHours;

	// �������� ��������� �� ������		
		MyDeviceCB PTR pCB = (MyDeviceCB PTR)pTaskCB->pDriverCB;
		RINTEGER iTimeout = GetSerialDrvIntegerProperty(pTaskCB, 0, 1, MYDEVICE_TIMEOUT);
		DRV_MODULE PTR pModule = GetSerialModule(pTaskCB, 0);// 0 - ����� ������. �� ���� com-���� - 1 ������
	// �������� �������� ����� "������"
		pParam = GetModuleParam(pModule, 3);// �������� ��� boolean
		iReadBoolean(pParam, &startFlag);

		//printf("Start Flag is %d \r\n", startFlag);//printf("Value is %d \r\n", pParam->Value);//printf("Quality is %d \r\n", pParam->Quality);
		if(startFlag == 1){
			pParam = GetModuleParam(pModule, 0);// �������� ��� integer
			iReadInteger(pParam, &imBusAddress);
			uchmBusAddress = (unsigned char)(imBusAddress);
		//������������ ������� ������ � pCB->SendBuf
			pCB->SendBuf[2] = uchmBusAddress; 
			pCB->SendBuf[3] = (unsigned char)(uchmBusAddress + 0x5b);
			RClearCom(pTaskCB->Port);
			RClearTxBuffer(pTaskCB->Port);
			RToComBufn(pTaskCB->Port, pCB->SendBuf, CMD_LENGTH);
			timeOutCycles = 6; // ���� 1.5 ���
			do{ //bytesRx = RDataSizeInCom(pTaskCB->Port); //if(bytesRx > 100) //break;
				RSleep_ms(250);
				timeOutCycles--;
			}while(timeOutCycles > 0);
			
			bytesRx = RReadComBufn(pTaskCB->Port, pCB->RecvBuf, ANSW_LENGTH);
			printf("Bytes - %d \r\n", bytesRx);
			if(bytesRx != 0){
				//printf("M-Bus Info - %.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x|%.2x \r\n", pCB->RecvBuf[0], pCB->RecvBuf[1], pCB->RecvBuf[2],\
				//													  pCB->RecvBuf[3], pCB->RecvBuf[4], pCB->RecvBuf[5],\
				//													  pCB->RecvBuf[6], pCB->RecvBuf[7], pCB->RecvBuf[8], pCB->RecvBuf[9],\
				//													  pCB->RecvBuf[10], pCB->RecvBuf[11], pCB->RecvBuf[12], pCB->RecvBuf[13]);
			
				memcpy(&opSerialNumb,  &pCB->RecvBuf[7], 4);
				memcpy(&opMegaCalories,  &pCB->RecvBuf[26], 4);
				memcpy(&opVolume,  &pCB->RecvBuf[32], 4);
				opFTemperature = 0;
				memcpy(&opFTemperature,  &pCB->RecvBuf[38], 2);
				opRTemperature = 0;
				memcpy(&opRTemperature,  &pCB->RecvBuf[42], 2);
				memcpy(&opKiloCalories,  &pCB->RecvBuf[57], 4);
				opVolFlow = 0;
				memcpy(&opVolFlow,  &pCB->RecvBuf[63], 2);
				memcpy(&opWorkHours,  &pCB->RecvBuf[75], 4);

				pParam = GetModuleParam(pModule, 6);  
				iWriteIntegerGood(pParam, opSerialNumb);
				pParam = GetModuleParam(pModule, 7);  
				iWriteIntegerGood(pParam, opMegaCalories);
				pParam = GetModuleParam(pModule, 8);  
				iWriteIntegerGood(pParam, opVolume);
				pParam = GetModuleParam(pModule, 9);  
				iWriteIntegerGood(pParam, opFTemperature);
				pParam = GetModuleParam(pModule, 10);  
				iWriteIntegerGood(pParam, opRTemperature);
				pParam = GetModuleParam(pModule, 11);  
				iWriteIntegerGood(pParam, opKiloCalories);
				pParam = GetModuleParam(pModule, 12);  
				iWriteIntegerGood(pParam, opVolFlow);
				pParam = GetModuleParam(pModule, 13);  
				iWriteIntegerGood(pParam, opWorkHours);

				printf("Data for Ser Numb %.8X \r\n", opSerialNumb);
				
			}

			pParam = GetModuleParam(pModule, 14); // �������������� �����
			iWriteIntegerGood(pParam, (RINTEGER)imBusAddress);

			pParam = GetModuleParam(pModule, 17); // Name="�������� ����"
			checkres = iWriteBooleanGood(pParam, (RBOOLEAN)1);
			if(checkres == S_OK){
				//PRINT0("Set flag to DONE OK");CR;
			}else{
				printf(" Result on POS_DONE cycle is not good - %ld \r\n", checkres);
			}
		}else{
			// �������� ������ "������"
			pParam = GetModuleParam(pModule, 17); // Name="�������� ����"
			checkres = iWriteBooleanGood(pParam, (RBOOLEAN)0);
			if(checkres == S_OK){
				//PRINT0("Set flag to ZERO OK");CR;
			}else{
				printf(" Result on ZERO cycle is not good - %ld \r\n", checkres);
			}
		}
		return S_OK;
	}

	if (mode == dmWrite || mode == dmWriteByChange)
	{	
		//PRINT0("JUST WRITE CYCLE");CR;
		return S_OK;
	}

	if (mode == dmDone){
		MyDeviceCB PTR pCB = (MyDeviceCB PTR) (pTaskCB->pDriverCB);
		FreeMem (pCB->pFloat);
		FreeMem (pCB->pBool);
		FreeMem (pCB->ids);		
		FreeMem (pTaskCB->pDriverCB);
		return S_OK;
	}
	
	return E_FAIL;
}
