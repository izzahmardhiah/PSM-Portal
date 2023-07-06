using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using psmportal.Models;

namespace psmportal.Models.ViewModels
{
    public class EvaluationProposalViewModel
    {
        public tb_evaluation Evaluation { get; set; }
        public tb_proposal Proposal { get; set; }
    }
}