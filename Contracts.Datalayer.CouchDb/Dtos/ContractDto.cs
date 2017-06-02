using System;
using System.Collections.Generic;

namespace Cmas.DataLayers.CouchDb.Contracts.Dtos
{
    public class ContractDto
    {
        /// <summary>
        /// Внутренний идентификатор
        /// </summary>
        public string _id;

        /// <summary>
        /// Внутренний идентификатор
        /// </summary>
        public string _rev;

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdatedAt;

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt;

        /// <summary>
        /// Название договора
        /// </summary>
        public string Name;

        /// <summary>
        /// Номер договора
        /// </summary>
        public string Number;

        /// <summary>
        /// Дата заключения
        /// </summary>
        public DateTime StartDate;

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime FinishDate;

        /// <summary>
        /// Имя подрядчика
        /// </summary>
        public string ContractorName;

        /// <summary>
        /// Стоимости договора
        /// </summary>
        public IList<AmountDto> Amounts;

        /// <summary>
        /// НДC включен в стоимость договора
        /// </summary>
        public bool VatIncluded;

        /// <summary>
        /// Название объекта строительства
        /// </summary>
        public string ConstructionObjectName;

        /// <summary>
        /// Название объекта строительства по титульному списку
        /// </summary>
        public string ConstructionObjectTitleName;

        /// <summary>
        /// Код титульного списка объекта строительства
        /// </summary>
        public string ConstructionObjectTitleCode;

        /// <summary>
        /// Примечания к договору
        /// </summary>
        public string Description;

        /// <summary>
        /// Системное имя шаблона для НЗ и TS
        /// </summary>
        public string TemplateSysName;
    }
}
